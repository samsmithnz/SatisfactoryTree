# Copilot Instructions

Scope
- This repository uses .NET 8 and Blazor WebAssembly. Apply these rules to all C# code in all projects.

Code style
- Do not use implicit typing. Never use `var`. Always use explicit types.
  - Example: `int count = 0;` instead of `var count = 0;`.
- Always include braces `{ }` for all control statements and loops, even when the body is a single statement.
  - Applies to: `if`, `else if`, `else`, `for`, `foreach`, `while`, `do`, `switch` sections, `using`, and `lock`.
  - Example:
    - Preferred:
      ```csharp
      if (condition)
      {
          DoWork();
      }
      else
      {
          HandleElse();
      }

      foreach (Item item in items)
      {
          Process(item);
      }
      ```
    - Avoid:
      ```csharp
      if (condition)
          DoWork();
      for (int i = 0; i < n; i++)
          Process(i);
      ```

Testing and testability
- Always create or update tests when adding features or fixing bugs.
  - Place unit tests in `SatisfactoryTree.Logic.Tests` where possible.
  - Use clear Arrange-Act-Assert structure; keep tests deterministic and fast.
- Keep code testable:
  - Move UI-independent logic into the `SatisfactoryTree.Logic` project (e.g., in `Services`).
  - Depend on interfaces in `SatisfactoryTree.Logic.Abstractions` and inject them (constructor injection); avoid service locators and statics.
  - Avoid hidden I/O, global state, singletons, and direct `DateTime.Now`/`Random`/`Console` calls in core logic; abstract these behind interfaces if needed.
  - Return values or expose state changes that can be asserted; avoid void side-effects where a result can be returned.
- For Blazor components:
  - Keep components thin; delegate business logic and calculations to services in Logic.
  - Preserve separation of concerns to enable testing without a Blazor runtime.
- When modifying behavior:
  - Include at least one regression test that covers the scenario.
  - Prefer small, focused units that can be mocked and verified.

Notes
- Keep generated samples, fixes, and refactors consistent with these rules.
- Prefer clarity over brevity in all suggested code.
