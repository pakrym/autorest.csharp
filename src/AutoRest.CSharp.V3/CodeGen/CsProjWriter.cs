namespace AutoRest.CSharp.V3.CodeGen
{
    internal class CsProjWriter : StringWriter
    {
        public bool WriteCsProj(Configuration configuration)
        {
            Line($@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.0</TargetFramework>
    <AssemblyName>{configuration.Namespace}</AssemblyName>
    <RootNamespace>{configuration.Namespace}</RootNamespace>
    <!-- Some methods are marked async and don't have an await in them -->
    <NoWarn>1998</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""Azure.Core"" Version=""1.0.0"" />
    <PackageReference Include=""NUnit"" Version=""3.12.0"" />
  </ItemGroup>

</Project>
");

            return true;
        }
    }
}
