using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RedisCacheApi.Data.FileRepos;
using RedisCacheApi.Data.FolderRepos;
using RedisCacheApi.Dtos.FolderDtos;
using RedisCacheApi.Dtos.SystemDtos;

namespace RedisCacheApi.Controllers
{
    [Route("api/system")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISqlFolderRepo _sqlFolderRepo;
        private readonly ISqlFileRepo _sqlFileRepo;
        private readonly INoSqlFileRepo _noSqlFileRepo;
        private readonly IMapper _mapper;

        public SystemController(ISqlFolderRepo sqlFolderRepo,
            ISqlFileRepo sqlFileRepo, INoSqlFileRepo noSqlFileRepo, IMapper mapper)
        {
            _sqlFolderRepo = sqlFolderRepo;
            _sqlFileRepo = sqlFileRepo;
            _noSqlFileRepo = noSqlFileRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SystemObjDto>> GetRoot()
        {
            var rootFolders = _sqlFolderRepo.GetAllFolders().Where(fold => fold.FolderForeign == null);
            var rootFiles = _sqlFileRepo.GetAllFiles()?.Where(fil => fil.FolderForeign == null);

            var folderSysObjs = _mapper.Map<IEnumerable<SystemObjDto>>(rootFolders);
            var fileSysObjs = _mapper.Map<IEnumerable<SystemObjDto>>(rootFiles);

            return Ok(folderSysObjs.Concat(fileSysObjs));
        }

        [HttpGet("{id}")]
        public ActionResult<FolderOpenDto> OpenFolderById(string id)
        {
            var folderModel = _sqlFolderRepo.GetFolderById(id);
            if (folderModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FolderOpenDto>(folderModel));
        }
    }
}
