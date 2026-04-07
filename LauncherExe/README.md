# LauncherExe

Launcher WinForms để:

- tải `manifest.json` từ URL
- kiểm tra SHA256
- tải file thiếu hoặc file cũ
- giải nén `.zip`
- mở game
- mở tool quản lý tài khoản

## Manifest mẫu

```json
{
  "version": "1.0.0",
  "files": [
    {
      "path": "Packages/game.zip",
      "url": "https://your-domain.com/modnro/game.zip",
      "sha256": "your_sha256_here",
      "extract": true,
      "extractTo": "Game"
    },
    {
      "path": "Tools/AccountManagerExe.exe",
      "url": "https://your-domain.com/modnro/AccountManagerExe.exe",
      "sha256": "your_sha256_here",
      "extract": false,
      "extractTo": ""
    }
  ]
}
```

## Gợi ý tổ chức file local

- `InstallFolder\Game\game.exe`
- `InstallFolder\Tools\AccountManagerExe.exe`

Trong launcher:

- `Game path` đặt là `Game\game.exe`
- `QLTK path` đặt là `Tools\AccountManagerExe.exe`
