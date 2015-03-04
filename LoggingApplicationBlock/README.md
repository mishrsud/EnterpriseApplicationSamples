##Synopsis

A Logging facade that abstracts away most common logging APIs, inspired from Common.Logging

Implementations should defer calling a message's object.ToString() until the message really needs
to be logged to avoid performance penalties.

Based on the ILog interface in Common.Logging authored by Mark Pollack, Bruno Baia and Erich Eichinger.

## Code Example
STEP 1: Configure your app.config like so:
```xml
<!-- App.config Start -->
<configuration>
	<configSections>
		<sectionGroup name="loggingApplicationBlock">
			<section name="logging" type="Logging.Impl.LoggingConfigurationSection, Logging.Impl"/>
		</sectionGroup>
	</configSections>
	<loggingApplicationBlock>
		<logging>
			<loggerFactory type="Logging.Impl.Concrete.NLogLoggerFactory, Logging.Impl">
				<arguments>
					<add name="configType" value="FILE"/>
					<add name="configFile" value="~\NLog.config"/>
				</arguments>
			</loggerFactory>
		</logging>
	</loggingApplicationBlock>
</configuration>
<!-- App.config End -->
```

STEP 2: Now, install NLog using NuGet in the project where logging is required:

Install-Package NLog -Version "3.2.0.0"

Next, add a config file for NLog by adding a new item to your project. The item would be of type Application configuration file and should be called NLog.config (if you call it by another name, you msut change the configFile parameter
in app.config
Add the following configuration to NLog.config:
```xml
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

	<variable name="ApplicationName" value="[Your Application Name here]"/>
	<variable name="LogDirectory" value="C:\Logs"/>

	<variable name="EventLogSource" value="${ApplicationName}"/>
	<variable name="LogFileName" value="${LogDirectory}\${ApplicationName}"/>
	<!-- For Layout render macros see: http://nlog-project.org/wiki/Layout_renderers -->
	<variable name="FileLayout" value="${longdate}|${machinename}|${processid}|${threadid}|${windows-identity}|${level:uppercase=true}|${event-properties:EventID}|${logger}|${message}|${exception:format=tostring}"/>	
	<variable name="EventLogLayout" value="Logged by: ${logger}${newline}${newline}${message}${newline}${exception:format=tostring}${newline}${newline}${newline}Process id: [${processId}]${newline}ThreadId: [${threadid}]${newline}Windows Identity: ${windows-identity}"/>

	<targets>
		<target name="file" type="AsyncWrapper">
			<target xsi:type="File"
						archiveEvery="Day"
						archiveAboveSize="10000000"
						maxArchiveFiles="10"
						archiveNumbering="Rolling"
						archiveFileName="${LogFileName}_${shortdate}_{##}.log"
						fileName="${LogFileName}.log"
						layout="${FileLayout}"
						concurrentWrites="false"/>
		</target>

		<target name="errorFile" type="AsyncWrapper">
			<target xsi:type="File"
						archiveEvery="Day"
						archiveAboveSize="10000000"
						maxArchiveFiles="10"
						archiveNumbering="Rolling"
						archiveFileName="${LogFileName}_ERROR_{##}.log"
						fileName="${LogFileName}_ERROR.log"
						layout="${FileLayout}"
						concurrentWrites="false"/>
		</target>

		<target name="eventlog" type="AsyncWrapper">
			<target xsi:type="EventLog"
							layout="${EventLogLayout}"
							source="${EventLogSource}"
							eventID="${event-properties:EventID}"
							log="Application"/>
		</target>
	</targets>

	<rules>
		<logger name="*" minlevel="Info" writeTo="eventlog" />
		<logger name="*" minlevel="Info" writeTo="file" />
		<logger name="*" minlevel="Error" writeTo="errorFile" />
	</rules>

</nlog>
```
STEP 4: You are ready for logging now! Obtain a logger and log like so:

```csharp
Logging.ILogger _logger = LogManager.GetCurrentClassLogger();
_logger.Debug( m=>m("result is {0}", random.NextDouble()) );
_logger.Debug(delegate(m) { m("result is {0}", random.NextDouble()); });
```
