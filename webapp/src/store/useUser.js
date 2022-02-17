import { defineStore } from 'pinia';
import jwtDecode from 'jwt-decode';

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
    getters: {
        isEmpty: (state) => {
            if (state.id === "" && state.email === "" && state.name === "" && state.avatar === "" && state.wallpaper === "" && state.accessToken === "") {
                return true;
            }

            return false;
        },
        localSave: (state) => {
            if (state.id === "") {
                try {
                    const localUser = JSON.parse(localStorage.getItem('user'));
                    const localToken = localStorage.getItem('accessToken');

                    // Check valid access token
                    if (!localToken) return null;

                    let decodedToken = jwtDecode(localToken);

                    if (decodedToken.exp * 1000 < Date.now()) {
                        localStorage.removeItem("accessToken");
                        localStorage.removeItem("user");
                        return null;
                    }

                    const user = {
                        id: localUser.id,
                        email: localUser.email,
                        name: localUser.name,
                        avatar: localUser.avatar,
                        wallpaper: localUser.wallpaper,
                        accessToken: localToken
                    };
                    return user;
                } catch { return null; }
            } else {
                return {
                    id: state.id,
                    email: state.email,
                    name: state.name,
                    avatar: state.avatar,
                    wallpaper: state.wallpaper,
                    accessToken: state.accessToken,
                };
            }
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
        login(user) {
            localStorage.setItem("accessToken", user.accessToken);
            localStorage.setItem("user", JSON.stringify({
                id: user.id,
                email: user.email,
                name: user.name,
                avatar: user.avatar,
                wallpaper: user.wallpaper,
            }))

            this.id = user.id;
            this.email = user.email;
            this.name = user.name;
            this.avatar = user.avatar;
            this.wallpaper = user.wallpaper;
            this.accessToken = user.accessToken;
        },
        logout() {
            localStorage.removeItem("accessToken");
            localStorage.removeItem("user");
        }
    }
});