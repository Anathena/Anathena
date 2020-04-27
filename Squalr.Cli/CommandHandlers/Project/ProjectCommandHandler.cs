﻿namespace Squalr.Cli.CommandHandlers.Project
{
    using CommandLine;
    using Squalr.Engine;
    using System;
    using System.Collections.Generic;

    public class ProjectCommandHandler : ICommandHandler
    {
        public String GetCommandName()
        {
            return "Projects";
        }

        public void TryHandle(ref Session session, Command command)
        {
            Parser.Default.ParseArguments<ProjectAddOptions, ProjectRemoveOptions, ProjectListOptions, ProjectToggleOptions>(command.Args)
                .MapResult(
                    (ProjectAddOptions options) => options.Handle(),
                    (ProjectRemoveOptions options) => options.Handle(),
                    (ProjectListOptions options) => options.Handle(),
                    (ProjectToggleOptions options) => options.Handle(),
                    errs => 1
                );

            command.Handled = true;
        }

        public IEnumerable<String> GetCommandAndAliases()
        {
            return new List<String>()
            {
                "proj",
                "project"
            };
        }
    }
    //// End class
}
//// End namespace
