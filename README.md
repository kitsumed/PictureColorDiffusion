# Story
PictureColorDiffusion was born after multiple attempts to color 2D grayscale images using and editing other open source projects, mainly manga and comics (such as GAN models with LAB channels). After poor results, I tried using Stable Diffusion's img2img generation, controlnet quickly joined the process and I switched to txt2img generation. Once I've found settings that generally worked well between models, I decided to automate the generation with a application, as well as adding some additional options on the application side to improve the end result.

## What is PictureColorDiffusion?
PictureColorDiffusion is a program that automate 2d colorization of drawings / manga / comics using Stable Diffusion's WebUI API, it's interrogation feature, the controlnet extension and other features on the application side.

## Requirements
* [AUTOMATIC1111 Stable Diffusion WebUI](https://github.com/AUTOMATIC1111/stable-diffusion-webui) (Can be run locally or remotly, like on google colabs)
    * Need to be run with the `--api` argument.
* [ControlNet extension for the Stable Diffusion WebUI](https://github.com/Mikubill/sd-webui-controlnet)
    * If you plan to use a SD model, you will need [ControlNet SD models](https://huggingface.co/lllyasviel/ControlNet-v1-1/tree/main). For SDXL, there is no official models, but [some were made by the community](https://huggingface.co/bdsqlsz/qinglong_controlnet-lllite/tree/main).
    * For more informations how to install and where to put the ControlNet models, [please read their own Wiki](https://github.com/Mikubill/sd-webui-controlnet/wiki/Model-download#installation).
* A SD / SDXL model related to 2D drawing or anime, preferably trained on danbooru tags, like [AOM3](https://huggingface.co/WarriorMama777/OrangeMixs/blob/main/Models/AbyssOrangeMix3/AOM3_orangemixs.safetensors).
    * This model need to be put into the `models\Stable-Diffusion` directory of the AUTOMATIC1111 Stable Diffusion WebUI.
    * A VAE model if there isn't one baked into the SD / SDXL model, for SD1.x based model like AOM3, [stabilityai mse-840000-ema VAE](https://huggingface.co/stabilityai/sd-vae-ft-mse-original/blob/main/vae-ft-mse-840000-ema-pruned.safetensors) seems to give good results. The VAE model need to be put into the `models\VAE` directory of the AUTOMATIC1111 Stable Diffusion WebUI.
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
    Perform image segmentation on the input picture with a YoloV8 onnx model to keep parts of the original image in the output image.
    I've created an example model for detecting speech bubbles [available on huggingface](https://huggingface.co/kitsumed/yolov8m_seg-speech-bubble/blob/main/model_dynamic.onnx).
> [!NOTE]
> The application does not offer the possibility of targeting specific classes from a YoloV8 model during image segmentation.

## FAQ / Questions
### Where to store YoloV8 models?
All YoloV8 models must be placed in the `models` directory, located in the same directory as the executable.
Only `onnx` model are supported.
### What SD / SDXL model to use in the webui?
I tried to make every modes of the application have somewhat good results with popular 2d/anime related models from [huggingface](https://huggingface.co/) and [civitai](https://civitai.com/). In the end, I realised that the results seems to depends of the following:
* Has the SD / SDXL model been trained on colored images resembling your grayscale image & prompt?
    * Example: A grayscale comic with manga mode, but the model does not know manga related words well enough.
* Did the PictureColorDiffusion mode you selectioned matches your grayscale image?
    * Example: Manga mode for a drawing could cause poor results and turn the drawing into a manga like image.

There are some work arounds, you could train a Lora with colored images of what you want specifically for your model, then use the Lora using the additional prompt section of the application (Format: `<lora:LORA_NAME_HERE:WEIGHT_HERE>`). 
You can also use the additional prompt & negative prompt section to add informations on what you are trying to colorize. 
Keeping the `Use interrogation` feature enabled can also help, as it's automatically adding additional information on what you are trying to colorize.
