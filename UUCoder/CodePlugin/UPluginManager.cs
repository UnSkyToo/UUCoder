using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodePlugin
{
    public class UPluginManager : IPlugin
    {
        private List<IPlugin> mPlugins;

        public UPluginManager(UUCoder coder)
            : base(coder)
        {
            mPlugins = new List<IPlugin>();
        }

        public void AddPlugin(IPlugin plugin)
        {
            mPlugins.Add(plugin);
        }

        public void RemovePlugin(IPlugin plugin)
        {
            mPlugins.Remove(plugin);
        }

        public override void Initialize()
        {
            foreach (IPlugin plugin in mPlugins)
            {
                plugin.Initialize();
            }
        }

        public override void Reset()
        {
            foreach (IPlugin plugin in mPlugins)
            {
                plugin.Reset();
            }
        }

        public override void Render(CodeRender.IRenderer renderer)
        {
            foreach (IPlugin plugin in mPlugins)
            {
                plugin.Render(renderer);
            }
        }
    }
}
