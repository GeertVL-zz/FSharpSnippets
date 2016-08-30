using System;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace FSharpSnippets.Common
{
  public abstract class CommonPackage : Package, IOleComponent
  {
    private IOleComponentManager _compMgr;

    protected override void Initialize()
    {
      _compMgr = (IOleComponentManager) GetService(typeof (SOleComponentManager));

      base.Initialize();
    }

    public int FReserved1(uint dwReserved, uint message, IntPtr wParam, IntPtr lParam)
    {
      throw new NotImplementedException();
    }

    public int FPreTranslateMessage(MSG[] pMsg)
    {
      return 0;
    }

    public void OnEnterState(uint uStateID, int fEnter)
    {
    }

    public void OnAppActivate(int fActive, uint dwOtherThreadID)
    {
    }

    public void OnLoseActivation()
    {
    }

    public void OnActivationChange(IOleComponent pic, int fSameComponent, OLECRINFO[] pcrinfo, int fHostIsActivating,
      OLECHOSTINFO[] pchostinfo, uint dwReserved)
    {
    }

    public int FDoIdle(uint grfidlef)
    {
      var onIdle = OnIdle;
      if (onIdle != null)
      {
        onIdle(this, new ComponentManagerEventArgs(_compMgr));
      }

      return 0;
    }

    internal event EventHandler<ComponentManagerEventArgs> OnIdle;

    public int FContinueMessageLoop(uint uReason, IntPtr pvLoopData, MSG[] pMsgPeeked)
    {
      return 1;
    }

    public int FQueryTerminate(int fPromptUser)
    {
      return 1;
    }

    public void Terminate()
    {
    }

    public IntPtr HwndGetWindow(uint dwWhich, uint dwReserved)
    {
      return IntPtr.Zero;
    }
  }

  class ComponentManagerEventArgs : EventArgs
  {
    private readonly IOleComponentManager _compMgr;

    public ComponentManagerEventArgs(IOleComponentManager compMgr)
    {
      _compMgr = compMgr;
    }

    public IOleComponentManager ComponentManager
    {
      get
      {
        return _compMgr;
      }
    }
  }
}
