[Back to readme](README.md)
# Manga Modes
The two images from the manga mode showcase were taken from ***Say Hello to Black Jack***, a manga in the public domain.

The results with these images are somewhat poor, but more recent manga typically yield better outcomes.
<table class="center">
    <tr style="font-weight: bolder;text-align:center;">
        <td>Original</td>
        <td>MangaSD</td>
        <td>MangaXL</td>
    </tr>
  <tr>
    <td>
        <img src="media\MangaMode\input\0.jpg" width="250" height="340">
    </td>
    <td>
        <img src="media\MangaMode\SD\0.jpg" width="250" height="340">
    </td>
    <td>
        <img src="media\MangaMode\XL\0.jpg" width="250" height="340">
    </td>
  </tr>


  <tr>
    <td>
        <img src="media\MangaMode\input\1.jpg" width="250" height="340">
    </td>
    <td>
        <img src="media\MangaMode\SD\1.jpg" width="250" height="340">
    </td>
    <td>
        <img src="media\MangaMode\XL\1.jpg" width="250" height="340">
    </td>
  </tr> 
  
</table>

<details>
<summary><h2>Configurations</h2></summary>

### MangaSD
* **Model**: [AOM3](https://huggingface.co/WarriorMama777/OrangeMixs/blob/main/Models/AbyssOrangeMix3/AOM3_orangemixs.safetensors)
* **VAE**: [stabilityai mse-840000-ema](https://huggingface.co/stabilityai/sd-vae-ft-mse-original/blob/main/vae-ft-mse-840000-ema-pruned.safetensors)
* **Clip Skip**: `2`

**ControlNet Configuration**

* **UNIT 1** : [control_v11p_sd15s2_lineart_anime](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15s2_lineart_anime.pth) ([YAML](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15s2_lineart_anime.yaml))
* **UNIT 2** : [control_v11p_sd15_softedge](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15_softedge.pth) ([YAML](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15_softedge.yaml))

**Inference Configuration**

- [ ] Enable controlnet lowvram
- [ ] Keep original picture size
- [x] Use interrogation
- [ ] Include metadata
- [x] Use YoloV8 segmentation
* **YoloV8 ONNX Model** : [manga_model](https://huggingface.co/kitsumed/yolov8m_seg-speech-bubble/blob/main/model_dynamic.onnx)
* **Seed** : `100`
* **Sampler** : Euler
* **Steps** : `12`

### MangaXL
* **Model**: [AbyssOrange XL Else](https://civitai.com/models/356201/abyssorange-xl-else)
* **VAE**: None
* **Clip Skip**: `2`

**ControlNet Configuration**

* **UNIT 1** : [MistoLine_rank256](https://huggingface.co/TheMistoAI/MistoLine/blob/main/mistoLine_rank256.safetensors)
* **UNIT 2** : [MistoLine_rank256](https://huggingface.co/TheMistoAI/MistoLine/blob/main/mistoLine_rank256.safetensors)

**Inference Configuration**

- [ ] Enable controlnet lowvram
- [ ] Keep original picture size
- [x] Use interrogation
- [ ] Include metadata
- [x] Use YoloV8 segmentation
* **YoloV8 ONNX Model** : [manga_model](https://huggingface.co/kitsumed/yolov8m_seg-speech-bubble/blob/main/model_dynamic.onnx)
* **Seed** : `100`
* **Sampler** : Euler
* **Steps** : `12`
</details>

# Drawing Modes
The two images from the drawing mode showcase are in the public domain.
<table class="center">
    <tr style="font-weight: bolder;text-align:center;">
        <td>Original</td>
        <td>DrawingSD</td>
        <td>DrawingXL</td>
    </tr>
  <tr>
    <td>
        <img src="media\DrawingMode\input\0.jpg" width="250" height="250">
    </td>
    <td>
        <img src="media\DrawingMode\SD\0.jpg" width="250" height="250">
    </td>
    <td>
        <img src="media\DrawingMode\XL\0.jpg" width="250" height="250">
    </td>
  </tr>


  <tr>
    <td>
        <img src="media\DrawingMode\input\1.jpg" width="250" height="250">
    </td>
    <td>
        <img src="media\DrawingMode\SD\1.jpg" width="250" height="250">
    </td>
    <td>
        <img src="media\DrawingMode\XL\1.jpg" width="250" height="250">
    </td>
  </tr> 
  
</table>

<details>
<summary><h2>Configurations</h2></summary>

### DrawingSD
* **Model**: [AOM3](https://huggingface.co/WarriorMama777/OrangeMixs/blob/main/Models/AbyssOrangeMix3/AOM3_orangemixs.safetensors)
* **VAE**: [stabilityai mse-840000-ema](https://huggingface.co/stabilityai/sd-vae-ft-mse-original/blob/main/vae-ft-mse-840000-ema-pruned.safetensors)
* **Clip Skip**: `2`

**ControlNet Configuration**

* **UNIT 1** : [control_v11p_sd15s2_lineart_anime](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15s2_lineart_anime.pth) ([YAML](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15s2_lineart_anime.yaml))
* **UNIT 2** : [control_v11p_sd15_softedge](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15_softedge.pth) ([YAML](https://huggingface.co/lllyasviel/ControlNet-v1-1/blob/main/control_v11p_sd15_softedge.yaml))

**Inference Configuration**

- [ ] Enable controlnet lowvram
- [ ] Keep original picture size
- [x] Use interrogation
- [ ] Include metadata
- [ ] Use YoloV8 segmentation
* **Seed** : `100`
* **Sampler** : Euler
* **Steps** : `12`

### MangaXL
* **Model**: [AbyssOrange XL Else](https://civitai.com/models/356201/abyssorange-xl-else)
* **VAE**: None
* **Clip Skip**: `2`

**ControlNet Configuration**

* **UNIT 1** : [MistoLine_rank256](https://huggingface.co/TheMistoAI/MistoLine/blob/main/mistoLine_rank256.safetensors)

**Inference Configuration**

- [ ] Enable controlnet lowvram
- [ ] Keep original picture size
- [x] Use interrogation
- [ ] Include metadata
- [ ] Use YoloV8 segmentation
* **Seed** : `100`
* **Sampler** : Euler
* **Steps** : `12`
</details>