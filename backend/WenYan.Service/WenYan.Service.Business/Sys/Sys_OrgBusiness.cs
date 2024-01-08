namespace WenYan.Service.Business
{
    public class Sys_OrgBusiness : BusRepository<Sys_Org>, ISys_OrgBusiness
    {
        public Sys_OrgBusiness(GDbContext context) : base(context)
        {
        }

        public async Task<List<OrgTreeDto>> GetAllAsync(string name, string status)
        {
            var orgs = this.GetQueryable<Sys_Org>(true).Select(s => new OrgTreeDto
            {
                Id = s.Id,
                ParentId = s.ParentId ?? "",
                Name = s.Name,
                Code = s.Code,
                Remark = s.Remark,
                Seq = s.Seq,
                Status = s.Status,
                ModifyTime = s.ModifyTime,
            })
                 .OrderBy(x => x.Seq)
                 .AsQueryable();
            if (!name.IsNullOrEmpty() || !status.IsNullOrEmpty())
            {
                return await orgs
                 .WhereIf(!name.IsNullOrEmpty(), x => x.Name.Contains(name))
                 .WhereIf(!status.IsNullOrEmpty(), x => x.Status == status)
                 .ToListAsync();
            }
            var treeOrgs = TreeHelper.GetBaseTreeList(orgs.ToList());
            return treeOrgs;
        }
    }
}
