#pragma checksum "C:\Users\anoth\OneDrive - Grand Canyon University\GCU\CST-452\Workspace\Capstone---Small-Ratings\SmallRatingsDemo\SmallRatings\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6fd85f747aa72443b302ff01f5c7d7acd200e866"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\anoth\OneDrive - Grand Canyon University\GCU\CST-452\Workspace\Capstone---Small-Ratings\SmallRatingsDemo\SmallRatings\Views\_ViewImports.cshtml"
using SmallRatings;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\anoth\OneDrive - Grand Canyon University\GCU\CST-452\Workspace\Capstone---Small-Ratings\SmallRatingsDemo\SmallRatings\Views\_ViewImports.cshtml"
using SmallRatings.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6fd85f747aa72443b302ff01f5c7d7acd200e866", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f639f2e6463a135a1c1f9bf8a432aa37e08c24d7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\anoth\OneDrive - Grand Canyon University\GCU\CST-452\Workspace\Capstone---Small-Ratings\SmallRatingsDemo\SmallRatings\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Landing";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""intro-body"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-8 mx-auto"">
                <h1 class=""brand-heading"">WELCOME</h1>
                <p class=""intro-text"">
                    The Premier Platform for Delivering Your Services!
                    <br>Now in Beta!
                </p>
                <h3>");
#nullable restore
#line 14 "C:\Users\anoth\OneDrive - Grand Canyon University\GCU\CST-452\Workspace\Capstone---Small-Ratings\SmallRatingsDemo\SmallRatings\Views\Home\Index.cshtml"
               Write(TempData["RegMessage"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>");
            WriteLiteral("\r\n                <a class=\"btn btn-link btn-circle\" role=\"button\" href=\"#about\"><i class=\"fa fa-angle-double-down animated\"></i></a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n</header>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
