namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using System;
    using System.Collections;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Hosting;

    [AspNetHostingPermission(SecurityAction.Demand, Level=AspNetHostingPermissionLevel.Medium), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.High)]
    public class ShopPathProvider : VirtualPathProvider
    {
        private ShopVirtualFile shopVirtualFile;

        public override bool FileExists(string virtualPath)
        {
            if (this.IsPathVirtual(virtualPath))
            {
                this.shopVirtualFile = new ShopVirtualFile(virtualPath);
                return this.shopVirtualFile.Exists;
            }
            return base.Previous.FileExists(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (this.IsPathVirtual(virtualPath))
            {
                CacheDependency dependency = new CacheDependency(ServerHelper.MapPath("/Plugins/Template/" + ShopConfig.ReadConfigInfo().TemplatePath + "/"));
                CacheDependency dependency2 = new CacheDependency(ServerHelper.MapPath("/Plugins/Template/" + ShopConfig.ReadConfigInfo().TemplatePath + "/User/"));
                AggregateCacheDependency dependency3 = new AggregateCacheDependency();
                dependency3.Add(new CacheDependency[] { dependency });
                dependency3.Add(new CacheDependency[] { dependency2 });
                return dependency3;
            }
            return base.Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (this.IsPathVirtual(virtualPath)) return this.shopVirtualFile;
            return base.Previous.GetFile(virtualPath);
        }

        protected override void Initialize()
        {
        }

        private bool IsPathVirtual(string virtualPath)
        {
            return VirtualPathUtility.ToAppRelative(virtualPath).StartsWith("~/ashx", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

