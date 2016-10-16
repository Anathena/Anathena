﻿namespace Ana.Source.Project.ProjectItems.ScriptTemplates
{
    using System;

    internal class LuaTemplates
    {
        public static String AddCodeInjectionTemplate(String currentScript, String moduleName, IntPtr moduleOffset)
        {
            String templateCode =
                "function OnActivate()" + "\n\t\n" +
                "\t" + "MyCheat()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function MyCheat()" + "\n\t\n" +
                "\t" + "local entry = Memory:GetModuleAddress(\"" + moduleName + "\") + 0x" + moduleOffset.ToString("x") + "\n" +
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

            return currentScript + templateCode;
        }

        public static String AddGraphicsOverlayTemplate(String currentScript)
        {
            String templateCode =
                "function OnActivate()" + "\n\t\n" +
                "\t" + "MyOverlay()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function MyOverlay()" + "\n\t\n" +
                "\t" + "Graphics:Inject()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "end";

            return currentScript + templateCode;
        }
    }
    //// End class
}
//// End namespace