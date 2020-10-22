using Microsoft.Extensions.CommandLineUtils;
using NinjaApplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();
            p.Run(args);

            return;

            var app = new CommandLineApplication();

            var interactive = app.Option("-i|--interactive", "Start interactive", CommandOptionType.NoValue);
            app.HelpOption("-?|-h|--help");

            app.OnExecute(() => {

                if (interactive.HasValue())
                {
                    var interactiveApp = new CommandLineApplication();
                    interactiveApp.Command("show", Show);
                    interactiveApp.Command("exit", Exit);
                    interactiveApp.Command("help", Help);
                    while (true)
                    {
                        Console.Write("ninja>");
                        var interactiveArgs = Console.ReadLine();

                        try
                        {
                            interactiveApp.Execute(interactiveArgs);
                        }
                        catch(Exception e)
                        {
                            //no
                        }
                    }
                }

                return 0;
            });

            try
            {
                app.Execute(args);
            }
            catch(CommandParsingException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void Run(string[] args)
        {
            var app = new CommandLineApplication();
            RootCommand.Configure(app);

            try
            {
                app.Execute(args);
            }
            catch (Exception e)
            {
                Environment.ExitCode = e.HResult;
            }
        }

        private static void Help(CommandLineApplication obj)
        {
            obj.OnExecute(() => {
                obj.Parent.ShowHelp();
                return 0;
            });
        }

        private static void Exit(CommandLineApplication obj)
        {
            obj.OnExecute(() => {
                Environment.Exit(0);
                return 0;
            });
        }

        private static void Show(CommandLineApplication obj)
        {
            obj.OnExecute(() => {
                Console.WriteLine("Listing...");
                return 0;
            });
        }
    }
}
