#pragma checksum "C:\Users\Igzyy\source\repos\Mono\ProjectMono\ProjectMono\Views\Error\HandleErrorCode.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be8de74235265948fc57fe91c359e9cad622d074"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Error_HandleErrorCode), @"mvc.1.0.view", @"/Views/Error/HandleErrorCode.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Error/HandleErrorCode.cshtml", typeof(AspNetCore.Views_Error_HandleErrorCode))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be8de74235265948fc57fe91c359e9cad622d074", @"/Views/Error/HandleErrorCode.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1187826aa178d9cade7fc5ce997a090959185c1f", @"/Views/_ViewImports.cshtml")]
    public class Views_Error_HandleErrorCode : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 2 "C:\Users\Igzyy\source\repos\Mono\ProjectMono\ProjectMono\Views\Error\HandleErrorCode.cshtml"
  
    ViewData["Title"] = "HandleErrorCode";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(95, 22, true);
            WriteLiteral("\n<h2>Ups ;)</h2>\n\n<h1>");
            EndContext();
            BeginContext(118, 20, false);
#line 9 "C:\Users\Igzyy\source\repos\Mono\ProjectMono\ProjectMono\Views\Error\HandleErrorCode.cshtml"
Write(ViewBag.ErrorMessage);

#line default
#line hidden
            EndContext();
            BeginContext(138, 10, true);
            WriteLiteral("</h1>\n<h1>");
            EndContext();
            BeginContext(149, 24, false);
#line 10 "C:\Users\Igzyy\source\repos\Mono\ProjectMono\ProjectMono\Views\Error\HandleErrorCode.cshtml"
Write(ViewBag.RouteOfException);

#line default
#line hidden
            EndContext();
            BeginContext(173, 5, true);
            WriteLiteral("</h1>");
            EndContext();
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