GIT_BRANCH_SOURCE=git rev-parse --abbrev-ref HEAD
git checkout main
git merge $GIT_BRANCH_SOURCE --no-ff --no-commit
git merge --abort
git checkout -b main_$GIT_BRANCH_SOURCE
git rebase main
