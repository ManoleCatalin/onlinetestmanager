How to use SonarQube

1) Open PowerShell
2) Set current working dir as solution dir 
3) Run & '..\SonarQube Scanner\MSBuild.SonarQube.Runner.exe' begin /k:"onlinetestmanager-project-key" /o:"onlinetestmanager-project-key" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="74d69312255c1b6f145bfb88e66bf4ce9b89924e"
4) Run & "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" /t:rebuild
5) Run & '..\SonarQube Scanner\MSBuild.SonarQube.Runner.exe' end /d:sonar.login="74d69312255c1b6f145bfb88e66bf4ce9b89924e"

A bat might replace this in the near future