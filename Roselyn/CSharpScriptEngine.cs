using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Roselyn
{
    public static class CSharpScriptEngine
    {
        private static ScriptState<object> scriptState;
        public static object Execute(string script, params Assembly[] assemblies)
        {
            if (assemblies != null && assemblies.Length > 0)
            {
                var option = ScriptOptions.Default.WithReferences(assemblies);
                scriptState = scriptState == null
                    ? CSharpScript.RunAsync(script, option).Result
                    : scriptState.ContinueWithAsync(script, option).Result;
            }
            else
                scriptState = scriptState == null
                    ? CSharpScript.RunAsync(script).Result
                    : scriptState.ContinueWithAsync(script).Result;

            return !string.IsNullOrEmpty(scriptState.ReturnValue?.ToString()) ? scriptState.ReturnValue : null;
        }
    }
}