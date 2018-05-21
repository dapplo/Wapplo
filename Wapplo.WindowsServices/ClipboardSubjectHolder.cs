using System.Reactive.Subjects;
using Dapplo.Windows.Clipboard;

namespace Wapplo.WindowsServices
{
    /// <summary>
    /// This class holds the clipboard subject
    /// </summary>
    public class ClipboardSubjectHolder
    {
        /// <summary>
        /// The subject to track clipboard changes
        /// </summary>
        public ISubject<ClipboardUpdateInformation> ClipboardUpdates { get; } = new Subject<ClipboardUpdateInformation>();
    }
}
