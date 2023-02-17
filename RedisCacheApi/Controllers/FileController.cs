using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RedisCacheApi.Data.FileRepos;
using RedisCacheApi.Data.FolderRepos;
using RedisCacheApi.Dtos.FileDtos;
using RedisCacheApi.Models;
using RedisCacheApi.Utility;

namespace RedisCacheApi.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ISqlFolderRepo _sqlFolderRepo;
        private readonly ISqlFileRepo _sqlFileRepo;
        private readonly INoSqlFileRepo _noSqlFileRepo;
        private readonly IMapper _mapper;

        public FileController(ISqlFolderRepo sqlFolderRepo,
            ISqlFileRepo sqlFileRepo, INoSqlFileRepo noSqlFileRepo, IMapper mapper)
        {
            _sqlFolderRepo = sqlFolderRepo;
            _sqlFileRepo = sqlFileRepo;
            _noSqlFileRepo = noSqlFileRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FileReadDto>> GetAllFiles()
        {
            var allFiles = _sqlFileRepo.GetAllFiles();

            return Ok(_mapper.Map<IEnumerable<FileReadDto>>(allFiles));
        }

        [HttpGet("{id}", Name = "GetFileById")]
        public ActionResult<FileReadDto> GetFileById(string id)
        {
            var fileCache = _noSqlFileRepo.GetFileById(id);
            if (fileCache != null)
            {
                if (!CacheCheck.ExpirationCheck(fileCache.LastTimeUsed))
                {
                    _sqlFileRepo.UpdateLastTimeUsed(fileCache.Id);
                    _noSqlFileRepo.DeleteFile(fileCache);
                }
                else
                {
                    _noSqlFileRepo.UpdateLastTimeUsed(fileCache);
                }

                return Ok(_mapper.Map<FileReadDto>(fileCache));
            }

            var fileModel = _sqlFileRepo.GetFileById(id);
            if (fileModel == null)
            {
                return NotFound();
            }

            if (CacheCheck.UseQuantityCheck(fileModel.LastTimeUsed))
            {
                _noSqlFileRepo.CreateFile(fileModel);
            }

            _sqlFileRepo.UpdateLastTimeUsed(fileModel);

            return Ok(_mapper.Map<FileReadDto>(fileModel));
        }

        [HttpPost]
        public ActionResult CreateFile(FileCreateDto fileCreateDto)
        {
            var fileModel = _mapper.Map<DbFile>(fileCreateDto);
            fileModel.FolderForeign = _sqlFolderRepo.GetFolderById(fileCreateDto.FolderId);

            _sqlFileRepo.CreateFile(fileModel);
            _sqlFileRepo.SaveChanges();

            var fileReadDto = _mapper.Map<FileReadDto>(fileModel);

            return CreatedAtRoute(nameof(GetFileById), new { fileReadDto.Id }, fileReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFile(string id, FileUpdateDto fileUpdateDto)
        {
            var fileModel = _sqlFileRepo.GetFileById(id);
            if (fileModel == null)
            {
                return NotFound();
            }

            _mapper.Map(fileUpdateDto, fileModel);
            fileModel.FolderForeign = _sqlFolderRepo.GetFolderById(fileUpdateDto.FolderId);
            _sqlFileRepo.UpdateFile(fileModel);
            _sqlFileRepo.SaveChanges();

            var fileCache = _noSqlFileRepo.GetFileById(id);
            if (fileCache != null)
            {
                _noSqlFileRepo.UpdateFile(fileModel);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatchFile(string id, JsonPatchDocument<FileUpdateDto> patchDoc)
        {
            var fileModel = _sqlFileRepo.GetFileById(id);
            if (fileModel == null)
            {
                return NotFound();
            }

            var fileToPatch = _mapper.Map<FileUpdateDto>(fileModel);
            patchDoc.ApplyTo(fileToPatch, ModelState);

            if (!TryValidateModel(ModelState))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(fileToPatch, fileModel);
            fileModel.FolderForeign = _sqlFolderRepo.GetFolderById(fileToPatch.FolderId);
            _sqlFileRepo.UpdateFile(fileModel);
            _sqlFileRepo.SaveChanges();

            var fileCache = _noSqlFileRepo.GetFileById(id);
            if (fileCache != null)
            {
                _noSqlFileRepo.UpdateFile(fileModel);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFile(string id)
        {
            var fileModel = _sqlFileRepo.GetFileById(id);
            if (fileModel == null)
            {
                return NotFound();
            }

            _sqlFileRepo.DeleteFile(fileModel);
            _sqlFileRepo.SaveChanges();

            var fileCache = _noSqlFileRepo.GetFileById(id);
            if (fileCache != null)
            {
                _noSqlFileRepo.DeleteFile(fileModel);
            }

            return NoContent();
        }
    }
}
