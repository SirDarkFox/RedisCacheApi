using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RedisCacheApi.Data.FileRepos;
using RedisCacheApi.Data.FolderRepos;
using RedisCacheApi.Dtos.FolderDtos;
using RedisCacheApi.Models;

namespace RedisCacheApi.Controllers
{
    [Route("api/folders")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly ISqlFolderRepo _sqlFolderRepo;
        private readonly ISqlFileRepo _sqlFileRepo;
        private readonly INoSqlFileRepo _noSqlFileRepo;
        private readonly IMapper _mapper;

        public FolderController(ISqlFolderRepo sqlFolderRepo, ISqlFileRepo sqlFileRepo, INoSqlFileRepo noSqlFileRepo, IMapper mapper)
        {
            _sqlFolderRepo = sqlFolderRepo;
            _sqlFileRepo = sqlFileRepo;
            _noSqlFileRepo = noSqlFileRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FolderReadDto>> GetAllFolders()
        {
            var allFolders = _sqlFolderRepo.GetAllFolders();

            return Ok(_mapper.Map<IEnumerable<FolderReadDto>>(allFolders));
        }

        [HttpGet("{id}", Name = "GetFolderById")]
        public ActionResult<FolderReadDto> GetFolderById(string id)
        {
            var folderModel = _sqlFolderRepo.GetFolderById(id);
            if (folderModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FolderReadDto>(folderModel));
        }

        [HttpPost]
        public ActionResult CreateFolder(FolderCreateDto folderCreateDto)
        {
            var folderModel = _mapper.Map<DbFolder>(folderCreateDto);
            folderModel.FolderForeign = _sqlFolderRepo.GetFolderById(folderCreateDto.FolderId);

            _sqlFolderRepo.CreateFolder(folderModel);
            _sqlFolderRepo.SaveChanges();

            var folderReadDto = _mapper.Map<FolderReadDto>(folderModel);

            return CreatedAtRoute(nameof(GetFolderById), new { folderReadDto.Id }, folderReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateFolder(string id, FolderUpdateDto folderUpdateDto)
        {
            var folderModel = _sqlFolderRepo.GetFolderById(id);
            if (folderModel == null)
            {
                return NotFound();
            }

            _mapper.Map(folderUpdateDto, folderModel);
            folderModel.FolderForeign = _sqlFolderRepo.GetFolderById(folderUpdateDto.FolderId);
            _sqlFolderRepo.UpdateFolder(folderModel);
            _sqlFolderRepo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatchFolder(string id, JsonPatchDocument<FolderUpdateDto> patchDoc)
        {
            var folderModel = _sqlFolderRepo.GetFolderById(id);
            if (folderModel == null)
            {
                return NotFound();
            }

            var folderToPatch = _mapper.Map<FolderUpdateDto>(folderModel);
            patchDoc.ApplyTo(folderToPatch, ModelState);

            if (!TryValidateModel(ModelState))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(folderToPatch, folderModel);
            folderModel.FolderForeign = _sqlFolderRepo.GetFolderById(folderToPatch.FolderId);
            _sqlFolderRepo.UpdateFolder(folderModel);
            _sqlFolderRepo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFolder(string id)
        {
            var folderModel = _sqlFolderRepo.GetFolderById(id);
            if (folderModel == null)
            {
                return NotFound();
            }

            _sqlFolderRepo.DeleteFolder(folderModel);
            _sqlFolderRepo.SaveChanges();

            return NoContent();
        }
    }
}
