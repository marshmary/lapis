import { defineStore } from "pinia";

export const useDarkMode = defineStore("darkMode", {
    state: () => ({
        darkMode: false,
    }),
    getters: {
        isDarkMode: (state) => {
            return state.darkMode;
        },
    },
    actions: {
        setDarkMode(value) {
            this.darkMode = value;
            localStorage.setItem("isDarkMode", value);
        },
    },
});
