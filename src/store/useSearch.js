import { defineStore } from "pinia";

export const useSearchStore = defineStore("searchStore", {
    state: () => {
        return {
            tags: [],
            orientation: "",
            color: {
                primary: "",
                secondary: "",
                tertiary: ""
            }
        }
    },
    getters: {
        getSearchValue: (state) => {
            return state;
        },
        isColorEmpty: (state) => {
            if (state.color.primary === "" && state.color.secondary === "" && state.color.tertiary === "") {
                return true;
            }
            return false;
        },
        isEmpty: (state) => {
            if (state.tags.length === 0 && state.color.primary === "" && state.color.secondary === "" && state.color.tertiary === "" && state.orientation === "") {
                return true;
            }
            return false;
        }
    },
    actions: {
        setTags(tag) {
            if (!this.tags.some(t => t.toLowerCase() === tag.toLowerCase()))
                this.tags.push(tag);
        },
        setOrientation(orientation) {
            this.orientation = orientation;
        },
        setPrimaryColor(primaryColor) {
            this.color.primary = primaryColor;
        },
        setSecondaryColor(secondaryColor) {
            this.color.secondary = secondaryColor;
        },
        setTertiaryColor(tertiaryColor) {
            this.color.tertiary = tertiaryColor;
        },
        removeTag(tag) {
            this.tags = this.tags.filter((t) => (t.toLowerCase() !== tag.toLowerCase()))
        },
        removeAllTag() {
            this.tags = [];
        },
        clearOrientationAndColor() {
            this.orientation = "";
            this.color.primary = "";
            this.color.secondary = "";
            this.color.tertiary = "";
        }
    }
})