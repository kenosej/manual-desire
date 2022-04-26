# Conventions

## General conventions

- everything in **English**
- code length should **not** be longer than **80** characters per line
- commit messages in **imperative**
- commit messages starting with a **lowercase** letter
- **no** commit messages should end with a period

## using Gitflow

- **"main"** branch is the production branch, marked with tags
(specifying version)
- **"feature-{your-feature-short-name}"** branch for developing features
(all the work is being done here, and later merged into "dev" branch)
- **"dev"** (development) branch is where all the "feature" branches
are being merged into
- **"release-{1.x}"** branch is NOT used for adding new features,
but for final touch before merging into "main" (and "dev")
- **"hotfix"** branch is used for quickly fixing issues on "main" branch (merge
has to be done in "main" and "dev" branches afterwards)

## Merging
- merging into **"dev"** branch should be done using **rebase**
- there should be only fast-forward merge into the **"main"** branch, but in
case of messing stuff up with **"hotfix"** branches and forgetting to merge
changes into both **"main"** and **"dev"** branches, proceed with
**rebase** to **preserve linear history**

## Branch and Tag naming convention

- **release** branches should be named: "**release-{MAJOR.MINOR.PATCH}**" where
**MAJOR**, **MINOR**, and **PATCH** represent the corresponding current version
numbers, e.g. **release-0.0.1**, **release-0.0.2**, **release-1.2.7**...
- **main tag** should consist of "**v{MAJOR.MINOR.PATCH}**",
e.g. if merge occurs when **"release"** branch is named "**release-1.1.7**",
then "**main tag**" should be named "**v1.1.7**"
- **hotfix** branches also add up to a version number,
e.g. if hotfix branch is based of a main branch with a tag "**v.2.1.9**" then
new merge into **"main"** from **"hotfix"** branch is named "**v.2.2.0**"
- there are **NO dvo-digit numbers** in **MINOR** and **PATCH**;
patches add up to 9, then they increment a **MINOR** by 1, e.g.
4.0.9 -> 4.1.0
(10*10 version numbers should be enough for release and hotfix branches)
- new **MAJOR** resets **MINOR** and **PATCH** to 0,
e.g. **2.7.7** -> **3.0.0**
