# Contribution guidelines

This project welcomes and appreciates contributions from all over
the world. We stand by our [code of conduct](./CODE_OF_CONDUCT.md),
please review before proceeding to contribute in any manner.

- [Issues](#issues)
- [Branches](#branches)
- [Commits](#commits)
- [Pull requests](#pull-requests)

## Issues
- :heavy_check_mark: DO use one of the issue templates, if appropriate
- :heavy_check_mark: DO write a complete and concise description
- :heavy_check_mark: DO add content to support your request (e.g. screenshots)
- :x: DO NOT submit issues without description
- :x: DO NOT add improper content, wording or any advertisement
- :x: DO NOT write extremely long issue titles

## Branches
- `main`: The main branch
- `develop`: Main branch for active development iterations
- `release/<version>`: Branches for releases
- `bug/<ticket number>-<short-description>`: Branches for bugs
- `feat/<ticket number>-<short-description>`: Branches for features
- `fix/<ticket number>-<short-description>`: Branches for **hotfixes** _(not bugfixes)_

## Commits
Example commit message: `feat: added support for DateTime serialization (#42)`

- :heavy_check_mark: DO use your full name in english format
(e.g first name last name)
- :heavy_check_mark: DO use a proper commit email
- :heavy_check_mark: DO attribute related issue numbers one-by-one
in parentheses on the end of the first line
- :heavy_check_mark: DO use a well-known commit type prefix
- :heavy_check_mark: DO add `[skip ci]` at the end of the commit message,
before issue attributions to skip ci runs
- :x: DO NOT write very long first lines in the commit message
- :x: DO NOT add scopes to a commit message
- :x: DO NOT use unknown commit types (see below)
- :x: DO NOT merge issue attribution of issues inside a single pair
of parentheses

As of now, signing commits is not required.

### Commit types

This project follows commit conventions inspired by
[angular](https://github.com/angular/angular/blob/master/CONTRIBUTING.md).

- `feat`: A new feature
- `fix`: A bug fix
- `docs`: Documentation only
- `perf`: Performance improvement
- `refactor`: A code change that neither fixes a bug nor adds a feature
- `ci`: Changes to CI configuration
- `build`: Changes that affect the build system or external dependencies
- `chore`: Configuration and etc
- `wip`: To explicitly signal work in progress _(a bot checks not to merge these)_
- :heavy_check_mark: DO add an exclamation mark `!` after the type
to mark the commit as `BREAKING CHANGES`
- :x: DO NOT use commit types not listed above

## Pull requests
- :heavy_check_mark: DO fill out the checklist in the template
- :heavy_check_mark: DO use the commit message as title, if the PR
contains only a single commit
- :heavy_check_mark: DO request review from the maintainer (@bencelang)
- :heavy_check_mark: DO use [closing keywords](https://docs.github.com/en/issues/tracking-your-work-with-issues/linking-a-pull-request-to-an-issue#linking-a-pull-request-to-an-issue-using-a-keyword)
- :x: DO NOT abandon a PR
