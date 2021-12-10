import { defineStore } from 'pinia';

export const useUserStore = defineStore("userStore", {
    state: () => {
        return {
            id: "",
            email: "",
            name: "",
            avatar: "",
            wallpaper: "",
            accessToken: ""
        }
    },
    actions: {
        updateAccessToken(newToken) {
            this.accessToken = newToken;
        },
        updateAvatar(newAvatar) {
            this.avatar = newAvatar;
        },
        updateWallpeper(newWallpaper) {
            this.wallpaper = newWallpaper;
        },
        setUserDetail(user) {
            this.id = user.id;
            this.email = user.email;
            this.name = user.name;
            this.avatar = user.avatar;
            this.wallpaper = user.wallpaper;
            this.accessToken = user.accessToken;
        }
    }
});