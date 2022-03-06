import { defineStore } from "pinia";

export const useDarkMode = defineStore("darkMode", {
    state: () => {
        return {
            default: false,
        };
    },
    getters: {
        isDarkMode: (state) => {
            const localDarkMode = localStorage.getItem("isDarkMode");
            return localDarkMode ? localDarkMode : state.default;
        },
    },
    actions: {
        setDarkMode: (value) => {
            localStorage.setItem("isDarkMode", value);
        },
    },
});
