using Newtonsoft.Json.Linq;
using SwarmUI.Builtin_ComfyUIBackend;
using SwarmUI.Core;
using SwarmUI.Text2Image;
using SwarmUI.Utils;

// NOTE: Namespace must NOT contain "SwarmUI" (this is reserved for built-ins)
namespace Yoinked.Extensions.Mahiron;

// NOTE: Classname must match filename
public class Mahiron : Extension // extend the "Extension" class in Swarm Core
{
    // Generally define parameters as "public static" to make them easy to access in other code, actual registration is done in OnInit

    public static T2IRegisteredParam<bool> RefinerEnabled, Enabled;

    // OnInit is called when the extension is loaded, and is the general place to register most things
    public override void OnInit()
    {
        Enabled = T2IParamTypes.Register<bool>(new(
            Name: "Enable Mahiron",
            Description: "Mahiro is so cute she deserves a better guidance function!! (。・ω・。)\nEnable MaHiRo. Changes CFG's behavior to make it cuter and smarter.",
            Default: "false", // Mahiro is too shy to be true by default
            Group: T2IParamTypes.GroupSampling,
            FeatureFlag: "comfyui", // it's apiui now
            OrderPriority: 8,
            ChangeWeight: 10,
            IgnoreIf: "false"
        ));

        WorkflowGenerator.AddModelGenStep(g =>
            {
                if (g.UserInput.Get(Enabled, false))
                {
                    string waifuNode = g.CreateNode("Mahiro", new JObject()
                    {
                        ["model"] = g.LoadingModel,
                    });
                    g.LoadingModel = [waifuNode, 0];
                }
            },
            priority: -20);

    }
}
