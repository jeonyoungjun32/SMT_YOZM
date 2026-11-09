# MES 빌드

Visual Studio 18 Community에서 `SNexis-MES.sln`을 열고 `Debug`, `Any CPU`로 빌드한다. 이미 열려 있다면 프로젝트 변경 사항을 다시 로드하거나 솔루션을 닫았다가 다시 연다.

DevExpress 23.2.12 참조는 저장소의 `../DLL` 폴더를 사용한다. 세 프로젝트의 HintPath가 이 폴더를 가리킨다. 기존 22.1 설치 경로로 참조를 변경하지 않는다.

최초 빌드 전 NuGet 패키지 복원을 수행한다. PowerShell에서 MES 폴더를 현재 위치로 사용한 예:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' .\SNexis-MES.sln /t:Restore /p:RestorePackagesConfig=true '/p:Platform=Any CPU'
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' .\SNexis-MES.sln /t:Rebuild /p:Configuration=Debug '/p:Platform=Any CPU'
```

2026-10-20 위 환경에서 전체 재빌드와 후속 빌드 성공 확인. 결과 파일은 `SmartMES/bin/Debug/SNexisMES_YOZM.exe`이다. 실행 시 DB 접속 설정은 별도로 필요하며 DB 연결 및 업무 기능 실행은 이번 빌드 검증에 포함하지 않았다.
