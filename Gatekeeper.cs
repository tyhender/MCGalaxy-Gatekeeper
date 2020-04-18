using System;
using MCGalaxy;
using MCGalaxy.Commands;
using MCGalaxy.Events.PlayerEvents;

namespace myServer {

  public sealed class GateKeeper : Plugin {
    public override string name    { get { return "&7Gate&aKeeper"; } }
    public override string creator { get { return "crush_"; } }
    public          string version { get { return "18.04.20"; } }
    public override string MCGalaxy_Version { get { return "1.9.1.9"; } }

    public override void Load(bool startup) {
      OnPlayerCommandEvent.Register(OnPlrCommand, Priority.Low);
			Chat.MessageGlobal(name + "loaded! Ver: " + version);
		}

    public override void Unload(bool shutdown) {
			Chat.MessageGlobal(name + "&cunloaded!");
		}


    void OnPlrCommand(Player subject, string cmd, string argString, CommandData data) {
       if (subject.group.Permission >= LevelPermission.Operator) { return; }
       string[] args = argString.SplitSpaces();

       if(cmd.CaselessEq("tp") || cmd.CaselessEq("teleport") ||
       cmd.CaselessEq("tpp") && data.Context != CommandContext.MessageBlock){

        if(subject.GetMotd().CaselessContains("-tp")){
          subject.Message(name + ": &cTeleportation isn't allowed here.");
          subject.cancelcommand = true;
          return;
        }

        if(args.Length == 1 ){
          Player tpObject = PlayerInfo.FindMatches(subject, args[0]);

          if(tpObject.GetMotd().CaselessContains("-tp")){
            subject.Message(name + ": &cTeleportation isn't allowed in " + tpObject.level.name);
            subject.cancelcommand = true;
            return;
          }
          
        }

       }

    }

  }

}
