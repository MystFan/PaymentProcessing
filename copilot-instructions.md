Copilot Instructions for PaymentProcessing repository

Purpose
- Provide guidelines for automated code edits and developer workflows for this repository.

Repository overview
- Solution: PaymentProcessing.slnx
- Projects: PaymentProcessing.Web, PaymentProcessing.Application, PaymentProcessing.Domain, PaymentProcessing.DataAccess, PaymentProcessing.IntegrationTests
- Target framework: .NET 10

Environment
- Developer IDE: Visual Studio 2026 (18.7.3)
- Preferred shell: powershell.exe
- Solution root: C:\Users\PC-TDIMITROV\Desktop\Projects\PaymentProcessing

Guidelines for automated changes (Copilot)
- Make minimal, targeted edits to fix the issue described by the user.
- Prefer built-in SDKs and existing project structure; do not introduce new frameworks unless requested.
- Always run a build after edits (dotnet build) and ensure it succeeds before finishing.
- When tests exist, run them and report failures (dotnet test). Run integration tests explicitly when requested.
- Use repository files and project files to discover structure (get_projects_in_solution, get_files_in_project, get_file) before making changes.
- Use apply_patch to modify files in the workspace. Create new files only when necessary.
- Do not change code style unless requested. If no style file exists, follow common C# conventions (consistent indentation, PascalCase for types, camelCase for parameters).

Commit & PR guidance
- Keep changes small and focused. One logical change per branch/PR.
- Update README or documentation if behavior or public API changes.

Contact
- This file is for automation guidance and should be updated if repository layout or conventions change.
