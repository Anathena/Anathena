namespace Ana.Source.LuaEngine
{
    using System;

    /// <summary>
    /// Defines Lua templates for various classifications of cheats
    /// </summary>
    internal class LuaTemplates
    {
        public static String GetCodeInjectionTemplate(String moduleName = "moduleName", UInt64 moduleOffset = 0x12345)
        {
            return "function OnActivate()" + "\n\t\n" +
                "\t" + "MyCheat()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function MyCheat()" + "\n\t\n" +
                "\t" + "local entry = Memory:GetModuleAddress(\"" + moduleName + "\") + 0x" + moduleOffset.ToString("x") + "\n" +
                "\t" + "Memory:SetKeyword(\"exit\", Memory:GetCaveExitAddress(entry))" + "\n\t\n" +

                "\t" + "local assembly = (" + "\n" +
                "\t" + "\n" +
                "\t" + "jmp exit" + "\n" +
                "\t" + ")" + "\n\t\n" +

                "\t" + "Memory:CreateCodeCave(entry, assembly)" + "\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "\t" + "Memory:ClearAllKeywords()" + "\n" +
                "\t" + "Memory:RemoveAllCodeCaves()" + "\n\t\n" +
                "end";
        }

        public static String GetGraphicsOverlayTemplate()
        {
            return "function OnActivate()" + "\n\t\n" +
                "\t" + "MyOverlay()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function MyOverlay()" + "\n\t\n" +
                "\t" + "Graphics:Inject()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "end";
        }
    }
    //// End class
}
//// End namespace