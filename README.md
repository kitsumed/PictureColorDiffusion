# Story
PictureColorDiffusion was born after multiple attempts to color 2D grayscale images using and editing other open source projects, mainly manga and comics (such as GAN models with LAB channels). After poor results, I tried using Stable Diffusion's img2img generation, controlnet quickly joined the process and I switched to txt2img generation. Once I've found settings that generally worked well between models, I decided to automate the generation with a application, as well as adding some additional options on the application side to improve the end result.

## What is PictureColorDiffusion?
PictureColorDiffusion is a program that automate 2d colorization of drawings / manga / comics using Stable Diffusion's WebUI API, it's interrogation feature, the controlnet extension and other features on the application side.

## Requirements
* [AUTOMATIC1111 Stable Diffusion WebUI](https://github.com/AUTOMATIC1111/stable-diffusion-webui) (Can be run locally or remotly, like on google colabs)
    * Need to be run with the `--api` argument.
* [ControlNet extension for the Stable Diffusion WebUI](https://github.com/Mikubill/sd-webui-controlnet)
> [!TIP]
> You can bypass the Stable Diffusion API Endpoint verification in the application with the shortcut `Ctrl+Shift+B`, keep in mind that some issues will arise if you do so. The colorization of images won't work, but you will be able to try your YoloV8 model by right clicking on the inference button. 
## Installation
**For AUTOMATIC1111 Stable Diffusion WebUI installation, [please read their own Wiki](https://github.com/AUTOMATIC1111/stable-diffusion-webui/wiki/).**

You can download the latest build of PictureColorDiffusion by clicking [here](https://github.com/kitsumed/PictureColorDiffusion/releases/latest/download/release.zip).
To run the application, unzip the `release.zip` file.

## Application Features
**This is a list of feature implemented directly in PictureColorDiffusion.**
* Dynamic resizing of image size depending of the selected mode.
*  #### YoloV8 image segmentation
    Perform image segmentation on the input picture with a YoloV8 onnx model to keep parts of the original image in the output image. All YoloV8 models must be placed in the `models` directory, located in the same directory as the executable.
    I've created an example model for detecting speech bubbles [available on huggingface](https://huggingface.co/kitsumed/yolov8m_seg-speech-bubble/blob/main/model_dynamic.onnx).
> [!NOTE]
> The application does not offer the possibility of targeting specific classes from a YoloV8 model during image segmentation.
