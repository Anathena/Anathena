using System;

namespace Anathema.Source.Project.ProjectItems.ScriptTemplates
{
    class LuaTemplates
    {
        public static String AddCodeInjectionTemplate(String CurrentScript, String ModuleName, IntPtr ModuleOffset)
        {
            String TemplateCode =
                "function OnActivate()" + "\n\t\n" +
                "\t" + "MyCheat()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function MyCheat()" + "\n\t\n" +
                "\t" + "local entry = Memory:GetModuleAddress(\"" + ModuleName + "\") + 0x" + ModuleOffset.ToString("x") + "\n" +
                "\t" + "Memory:SetKeyword(\"exit\", Memory:GetCaveExitAddress(entry))" + "\n\t\n" +

                "\t" + "local assembly = (" + "\n" +
                "\t" + "[fasm]" + "\n" +
                "\t" + "\n" +
                "\t" + "jmp exit" + "\n" +
                "\t" + "[/fasm])" + "\n\t\n" +

                "\t" + "Memory:CreateCodeCave(entry, assembly)" + "\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "\t" + "Memory:ClearAllKeywords()" + "\n" +
                "\t" + "Memory:RemoveAllCodeCaves()" + "\n\t\n" +
                "end";

            return CurrentScript + TemplateCode;
        }

        public static String AddGraphicsOverlayTemplate(String CurrentScript)
        {
            String TemplateCode =
                "function OnActivate()" + "\n\t\n" +
                "\t" + "MyOverlay()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function MyOverlay()" + "\n\t\n" +
                "\t" + "Graphics:Inject()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "end";

            return CurrentScript + TemplateCode;
        }

    } // End class

} // End namespace