using System.Linq;
using Content.Shared._CorvaxNext.Skills;
using Robust.Shared.Console;

namespace Content.Server._CorvaxNext.Skills.Commands;

public sealed class RevokeSkillCommand : IConsoleCommand
{
    [Dependency] private readonly ILocalizationManager _localization = default!;
    [Dependency] private readonly IEntityManager _entity = default!;

    public string Command => "revokeskill";

    public string Description => "Revokes skill from given entity.";

    public string Help => "revokeskill <entityuid> <skill>";

    public void Execute(IConsoleShell shell, string arg, string[] args)
    {
        if (args.Length != 2)
        {
            shell.WriteError(_localization.GetString("shell-wrong-arguments-number"));
            return;
        }

        if (!int.TryParse(args[0], out var id))
        {
            shell.WriteError(_localization.GetString("shell-entity-uid-must-be-number"));
            return;
        }

        if (!_entity.TryGetEntity(new(id), out var entity))
        {
            shell.WriteError(_localization.GetString("shell-invalid-entity-id"));
            return;
        }

        if (!Enum.TryParse<Shared._CorvaxNext.Skills.Skills>(args[1], out var skill))
        {
            shell.WriteError("No such skill.");
            return;
        }

        if (!_entity.TryGetComponent<SkillsComponent>(entity.Value, out var component))
            return;

        component.Skills.Remove(skill);
    }

    public CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        if (args.Length == 1)
            return new([.. _entity.GetEntities().Select(entity => entity.Id.ToString()).Where(str => str.StartsWith(args[0])).Select(entity => new CompletionOption(entity))], "entityuid");

        if (args.Length == 2)
            return new([.. Enum.GetNames<Shared._CorvaxNext.Skills.Skills>().Where(name => name.StartsWith(args[1])).Select(name => new CompletionOption(name))], "skill");

        return new([], null);
    }
}
