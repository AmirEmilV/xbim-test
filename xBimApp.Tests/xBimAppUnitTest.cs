using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using xBimApp.Api;
using xBimApp.Api.Controllers;
using xBimApp.Api.Services;
using XBimApp.Api;

namespace xBimApp.Tests
{
	/// <summary>
	/// xBimApp Unit Test
	/// </summary>
	public class xBimAppUnitTest
	{
    /// <summary>
    /// The configuration
    /// </summary>
    private IConfigurationRoot _config;
    private string _filePath = @"E:\Learning\xBim-Task\SampleHouse4.ifc";
    private string _fileName="SampleHouse4.ifc";
		/// <summary>
		/// The options
		/// </summary>
		private IOptions<AppSettings> _options;
    /// <summary>
    /// The env settings
    /// </summary>
    private EnviromentSetting _envSettings;

    /// <summary>
    /// Setups this instance.
    /// </summary>
    [SetUp]
    public void Setup()
    {
			var builder = new ConfigurationBuilder()
						 .SetBasePath(Directory.GetCurrentDirectory())
						 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
						 .AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true)
						 .AddEnvironmentVariables();
			_config = builder.Build();
			var settings = new AppSettings();
			_config.Bind(settings);
			_options = Options.Create<AppSettings>(settings);

			_envSettings = new EnviromentSetting
			{
				AwsS3Settings = new AwsS3Settings
				{
					AccessKeyId = "AKIASTSFBEODVXQRSZS2",
					SecretAccessKey = "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",
					BucketName = "xbim-ifc-files"
				}
			};
		}

    /// <summary>
    /// Collects the elements test.
    /// </summary>
    [Test]
    public async Task Collect_Elements_Test()
    {
      IActionResult? result = null;

			var mockawsService = new Mock<AWSService>(_envSettings);

			var mockModelElementsService = new Mock<ModelElementsService>(_options, _envSettings, mockawsService.Object);

			ModelElementsController modelElementsController = new ModelElementsController(mockModelElementsService.Object);


			result = await modelElementsController.GetElements(_fileName);

			Assert.IsInstanceOf<OkObjectResult>(result);
		}

    /// <summary>
    /// Collects the rooms test.
    /// </summary>
    [Test]
		public async Task Collect_Rooms_Test()
		{
			IActionResult? result = null;

			var mockawsService = new Mock<AWSService>(_envSettings);

			var mockIfcRoomsService = new Mock<IfcRoomsService>( _envSettings, mockawsService.Object);

			IfcRoomsController modelElementsController = new IfcRoomsController( mockIfcRoomsService.Object);

			result =await modelElementsController.GetRooms(_fileName);

			Assert.IsInstanceOf<OkObjectResult>(result);
		}
    /// <summary>
    /// Uploads the ifc test.
    /// </summary>
    [Test]
		public async Task Upload_Ifc_test()
		{
			IActionResult? result = null;


			//FileStream stream = new FileStream(@"E:\Learning\xBim-Task\SampleHouse4.ifc", FileMode.Open);

			//IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", name)
			//{
			//	Headers = new HeaderDictionary(),
			//	ContentType = "ifc/ifc"
			//};

			var mockawsService = new Mock<AWSService>(_envSettings);

			var mockIfcFileService = new Mock<IfcFileService>(_envSettings, mockawsService.Object);

			IfcFileController ifcFileController = new IfcFileController(mockIfcFileService.Object);


			result = await ifcFileController.UploadIFcFile(new UploadFileRequest()
			{
				FileName = _fileName,
				IFCFilePath =_filePath
			});

			Assert.IsInstanceOf<OkObjectResult>(result);
		}

    /// <summary>
    /// Reads the ifc test.
    /// </summary>
    [Test]
		public async Task Read_Ifc_test()
		{
			IActionResult? result = null;

			var name = "SampleHouse4.ifc";

			var mockawsService = new Mock<AWSService>(_envSettings);

			var mockIfcFileService = new Mock<IfcFileService>( _envSettings, mockawsService.Object);

			IfcFileController ifcFileController = new IfcFileController(mockIfcFileService.Object);


			result = await ifcFileController.LoadIFcFile(name);

			Assert.IsInstanceOf<OkObjectResult>(result);
		}
	}
}