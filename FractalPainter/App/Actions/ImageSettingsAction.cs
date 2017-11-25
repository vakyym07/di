﻿using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
	public class ImageSettingsAction : IUiAction
	{
	    private readonly ImageSettings imageSettings;
	    private IImageHolder imageHolder;

	    public ImageSettingsAction(ImageSettings imageSettings, IImageHolder imageHolder)
	    {
	        this.imageSettings = imageSettings;
	        this.imageHolder = imageHolder;
	    }

		public string Category => "Настройки";
		public string Name => "Изображение...";
		public string Description => "Размеры изображения";

		public void Perform()
		{
			SettingsForm.For(imageSettings).ShowDialog();
			imageHolder.RecreateImage(imageSettings);
		}
	}
}