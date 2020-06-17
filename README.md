# HBS Internal Baseline Install
This Baseline has a complete Kentico 12 Site and database export, you should be able to clone this completely, follow these steps when starting a new site

## Setup Instructions

1. Clone entire solution (remove the .git folder once cloned)
1. Remove the .gitignore and rename "production.gitignore" to ".gitignore"
1. Restore the .bak Database, and create and configure the Database Login / Security
1. Add the new Connection String to the CMS and Baseline's ConnectionString.config
1. Edit the `CMSHashStringSalt` in the `AppSettings.config` needs a unique GUID added, do this in both the CMS and Boilerplate folder's AppSettings.config
1. Adjust any other AppSettings (like CI Restore location path and admin login url)
1. Setup your IIS and license and such.
1. Push up to your own Github

## Contribution Rules
If you want to adjust the baseline, make a new branch, do all your stuff, and then re-back up the database, and do a pull request into the master.

## TO DO LIST
1. Froala Form Input Component when available
1. Object Selector when available
1. Ecommerce tools

## Installed Tools
### x.PagedList
[x.PagedList](https://github.com/dncuug/X.PagedList) is a pager that allows for easy pagination creation. You can either use a Url.Action for the page, or use our Url.RenderPath for relative urls (dynamic routing).  This handles the Query String values that your tool must then account for.

### Dynamic Routing
[Dynamic Routing](https://github.com/KenticoDevTrev/DynamicRouting) module is installed for easy routing.

### Kentico MVC Caching
[MVC Caching](https://github.com/KenticoDevTrev/MVCCaching) is installed.  

AutoFac Automatic registrations
1. Classes with `IRepository` or `IService` with methods that start with `Get` and are of types ITreeNode, IEnuemrable<ITreeNode>, BaseInfo, or IEnumerable<BaseInfo> will have automatic caching enabled on them (can manually set as well through `[CacheDependency("")]`, note that ##SITENAME## will be resolved in the cachedependency automatically, and {0} {1} etc can be used for the property value passed into that method. [See example](https://github.com/KenticoDevTrev/MVCCaching/blob/master/MVCCaching.Kentico.Examples/Repositories/Implementations/KenticoExamplePageTypeRepository.cs))
1. Classes with `ICacheHelper` property in constructor will be assigned Kentico's default CacheHelper
1. Classes with `IOutputCacheDependencies` property in constructor will be assigned the default OutputCacheDependencies, you can use this to call AddCacheItemDependencies to hook into Kentico's cache key handler.
1. Classes with `IRepository` interface if the constructor has a property of type string with name `cultureName` it will be passed the current Culture
1. Classes with `IRepository` interface if the constructor has a property of type bool with name `latestVersionEnabled` it will automatically pass if the Preview is Enabled.

`[ActionResultCache]` is also a caching attribute you can use on your ActionResult methods which will cache the logic to build the view model, but not cache the view.  For the most part though, as long as each widget/partial on the page adds it's own Dependencies to the IOutputCacheDependency, any change will clear the full [OutputCache] for that view.

## More information

For more information, please see me at www.devtrev.com

Sincerely,
   Trevor Fayas - Kentico MVP
