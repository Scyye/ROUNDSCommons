using System;
using System.Collections.Generic;
using System.Text;

namespace ROUNDSCommons.Commands
{
    internal class StatCommands
    {
        public class SetStat : Command
        {
            public override CommandDetails Details => new CommandDetails
            {
                Name="set-stat"
            };

            public override CommandResponse Execute(CommandEvent e)
            {
                string stat = e.args[0];
                string val = e.args[1];
                
                // TODO: IMPLEMENT THIS 

                throw new NotImplementedException();
            }
        }
    }
}
