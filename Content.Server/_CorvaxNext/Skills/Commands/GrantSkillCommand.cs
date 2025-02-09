using Content.Shared._CorvaxNext.Skills;
using Robust.Shared.Console;

namespace Content.Server._CorvaxNext.Skills.Commands;

public sealed class GrantSkillCommand : IConsoleCommand
{
    [Dependency] private readonly ILocalizationManager _localization = default!;
    [Dependency] private readonly IEntityManager _entity = default!;

    public string Command => "grantskill";

    public string Description => "Grants skill to given entity.";

    public string Help => "grantskill <entityuid> <skill>";

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

        var component = _entity.EnsureComponent<SkillsComponent>(entity.Value);

        component.Skills.Add(skill);
    }
}
