@echo off

rem Raffael Moser
rem The Pigeon Protocol

rem Butler Push
rem Add to post build event of project to automatically zip and push the build to itch.io
rem Needs a 7z.exe at the root of the repo
rem Events: post build event -> check master branch -> zip release folder -> push to itch

goto comment
rem Add this to the post-build event of your project

@echo off

set account=account-name
set project=project-name
set channel=channel-name

set configuration=$(ConfigurationName)
rem "Release"
set solutionDir=$(SolutionDir)
rem Directory of solution.sln
set targetDir=$(TargetDir)
rem Directory where build is located, probably Release folder

call %solutionDir%ButlerPush.bat %account% %project% %channel% %configuration% %solutionDir% %targetDir%

:comment


set account=%1
set project=%2
set channel=%3

set configuration=%4
set solutionDir=%5
set targetDir=%6

echo account: %account%
echo project: %project%
echo channel: %channel%

echo config: %configuration%
echo solution: %solutionDir%
echo target: %targetDir%


where 7z.exe
if errorlevel 1 (
    if not exist %solutionDir%7z.exe (
        if not exist .\7z.exe (
            echo Could not find 7z.exe!
            exit /b 0
        )
        else (
            set %solutionDir%=.\
        )
    )
)

where butler.exe
if errorlevel 1 (
    echo Could not find butler.exe!
    exit /b 0
)

butler.exe status %account%/%project%

call git branch --show-current | find /v "" | findstr /r /c:"^master$" > nul & if errorlevel 1 (
    echo "BUTLER NOT ON MASTER"
    exit /b 0
) else (
    echo "BUTLER ON MASTER"
    if %configuration% == Release (
        echo "BUTLER PUSHING"
        7z.exe a %targetDir%..\%project%.zip %targetDir%*
        butler.exe push %targetDir%..\%project%.zip %account%/%project%:%channel%
    )
)