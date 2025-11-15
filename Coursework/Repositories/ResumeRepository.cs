using DAL.Entities;
using DAL.Storage;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ResumeRepository : JsonRepositoryBase<ResumeEntity>, IResumeRepository
    {
        private const string FileName = "resumes.json";

        public ResumeRepository(FileDataContext context) : base(context, FileName) { }
    }
}
