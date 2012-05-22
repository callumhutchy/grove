﻿namespace Grove.Ui
{
  public interface IIsDialogHost
  {
    void AddDialog(object dialog, DialogType dialogType);
    void RemoveDialog(object dialog);
    bool HasFocus(object dialog);
  }
}