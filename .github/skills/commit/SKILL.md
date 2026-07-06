---
name: commit
description: Create a git commit after verifying that any active OpenSpec change has been archived. Use when the user wants to commit their work.
license: MIT
metadata:
  author: custom
  version: "1.0"
---

Create a git commit, but first verify that the OpenSpec workflow is complete.

**Input**: Optionally a commit message. If omitted, generate one based on staged changes.

**Steps**

1. **Check for active OpenSpec changes**

   Run `openspec list --json` to check if there are any active (non-archived) changes.

   Parse the JSON output to identify changes that are NOT in the archive directory.

   **If no active changes exist:** Skip to step 3.

   **If active changes exist:** Proceed to step 2.

2. **Warn about unarchived changes**

   For each active change, run:
   ```bash
   openspec status --change "<name>" --json
   ```

   Check the task completion status by reading the tasks file. Count tasks marked `- [ ]` (incomplete) vs `- [x]` (complete).

   **If all tasks are complete:**
   - Use the **AskUserQuestion tool** to warn:
     > "Change '<name>' has all tasks complete but is not yet archived. Do you want to archive it before committing?"
   - Options: "Archive first (recommended)", "Commit without archiving", "Cancel"
   - If user chooses archive: invoke the openspec-archive-change skill, then continue to step 3.

   **If tasks are still incomplete:**
   - Use the **AskUserQuestion tool** to warn:
     > "Change '<name>' still has incomplete tasks (X/Y done). Are you sure you want to commit?"
   - Options: "Commit anyway (partial progress)", "Cancel"
   - If user cancels: stop execution.

3. **Check git status**

   ```bash
   git status --short
   ```

   **If nothing to commit:** Inform the user there are no changes to commit and stop.

   **If there are unstaged changes but nothing staged:**
   - Show the list of modified/untracked files
   - Use the **AskUserQuestion tool** to ask:
     > "Nothing is staged. Do you want to stage all changes?"
   - Options: "Stage all", "Let me select manually", "Cancel"
   - If "Stage all": run `git add -A`
   - If "Let me select manually": stop and tell the user to stage files first

4. **Generate or use commit message**

   **If the user provided a commit message:** Use it as-is.

   **If no message provided:**
   - Run `git diff --cached --stat` to see what's staged
   - Run `git diff --cached` to see the actual changes
   - Generate a conventional commit message based on the changes:
     - Use format: `type(scope): description`
     - Types: feat, fix, refactor, test, docs, chore, style, perf
     - Keep the subject line under 72 characters
     - Add a body if the change is non-trivial
   - Show the generated message and use the **AskUserQuestion tool** to confirm:
     > "Proposed commit message:"
     > ```
     > <message>
     > ```
   - Options: "Use this message", "Let me edit"
   - If "Let me edit": ask for new message via **AskUserQuestion tool**

5. **Perform the commit**

   ```bash
   git commit -m "<message>"
   ```

   Show the commit result (hash, message, files changed).

**Output On Success**

```
## Committed

**Hash:** <short-hash>
**Message:** <commit-message>
**Files:** N files changed, X insertions(+), Y deletions(-)
**OpenSpec:** ✓ No active changes (or "⚠ Change '<name>' still active")
```

**Guardrails**
- Never skip the OpenSpec archive check
- Never use `--no-verify` or skip git hooks
- Always show what will be committed before committing
- Use conventional commit format for generated messages
- Don't force-push or amend without explicit user request
