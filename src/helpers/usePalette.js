import * as Vibrant from "node-vibrant";
import { ref } from "vue";
import { TONES } from "@/helpers/Constants";
import * as ColorThief from "@agencyanalytics/colorthief/dist/color-thief.js";

export const usePalette = () => {
    const primaryColor = ref("");
    const secondaryColor = ref("");
    const tertiaryColor = ref("");

    // convert image to base 64 then call detect color
    function getCollorsFromFile(fileItem) {

        const reader = new FileReader();
        if (fileItem) {
            reader.readAsDataURL(fileItem);
        }

        reader.addEventListener("load", () => {
            // Get primary color
            ColorThief.getColor(reader.result).then(color => {
                primaryColor.value = getToneColor(rgbToHex(color[0], color[1], color[2]));
                console.log("🚀 ~ file: usePalette.js ~ line 23 ~ ColorThief.getColor ~ rgbToHex(color[0], color[1], color[2])", rgbToHex(color[0], color[1], color[2]))
            })

            // Get secondary & tertiary color
            detectColor(reader.result);
        }, false);
    }

    // using vibrant lib to detect palette from base 64 image
    function detectColor(imgSrc = "") {
        const colors = [];

        // Try to get secondary and teriary color
        Vibrant.from(imgSrc)
            .maxColorCount(256)
            .getPalette()
            .then((palette) => {
                for (let color in palette) {
                    const type = color;
                    const hex = palette[color].getHex();
                    colors.push({ hex, type });
                }

                // Set color to return 
                secondaryColor.value = getToneColor(colors[0].hex);
                tertiaryColor.value = getToneColor(colors[3].hex);
            });
    }

    // Change detect color to selected tone
    function getToneColor(hex) {
        let minDeltaE = 100;
        let matchTone = "";

        for (let tone in TONES) {
            const diffDeltaE = Vibrant.Util.hexDiff(hex, TONES[tone]);
            if (diffDeltaE < minDeltaE) {
                // Restrict color can set to base tone
                if ((tone === "black" || tone === "white" || tone === "lightGray") && diffDeltaE < 12.8) {
                    minDeltaE = diffDeltaE;
                    matchTone = TONES[tone];
                    continue;
                }

                if (tone === "coffee" && diffDeltaE < 13.5) {
                    minDeltaE = diffDeltaE;
                    matchTone = TONES[tone];
                    continue;
                }

                minDeltaE = diffDeltaE;
                matchTone = TONES[tone];
            }
        }

        return matchTone;
    }

    // convert rgb to hex color
    const rgbToHex = (r, g, b) => '#' + [r, g, b].map(x => {
        const hex = x.toString(16)
        return hex.length === 1 ? '0' + hex : hex
    }).join('')

    // List of colors and get color form a file method
    return { primaryColor, secondaryColor, tertiaryColor, getCollorsFromFile }
}