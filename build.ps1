Properties {
	$baseDir = Resolve-Path .
	$project = Split-Path $baseDir -Leaf
	$artifactDir = "$baseDir\Build\Packages"
	$version = "1.0.0"
	$decription = "bla bla bla"
}

Task Default -Depends Build

Task Clean {
	if (Test-Path "$baseDir\build") {
		Remove-Item "$baseDir\Build" -Recurse -Force -ErrorAction Stop
	}
}
task RestorePackages {
	Exec { .\.nuget\NuGet restore -PackagesDirectory .\packages }
}
Task Build -Depends Clean,RestorePackages {
	Exec { msbuild "$project.sln" /p:Configuration=Release /t:"Clean,Build" }
}
Task Package -Depends Build {
	if (!(Test-Path($artifactDir))) {
		New-Item -ItemType Directory -Force -Path $artifactDir
	}
	Exec { .\.nuget\NuGet pack $baseDir\$project\$project.csproj -OutputDirectory $artifactDir -Properties Configuration=Release -Version $version }
}

Task Publish -Depends Package {
	Exec { .\.nuget\NuGet push "$artifactDir\$project.$version.nupkg" }
}