using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapplo.Language;

namespace Wapplo.Languages
{
	[Language("ContextMenu")]
	public interface IContextMenuTranslations : ILanguage, INotifyPropertyChanged
	{
		string Configure { get; }
		string Exit { get; }
		string Title { get; }
	}
}
