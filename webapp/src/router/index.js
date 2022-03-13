import { createRouter, createWebHistory } from "vue-router";
import { useUserStore } from "@/store/useUser";

const routes = [
    {
        path: "/",
        name: "Home",
        component: () => import("@/views/Home.vue"),
    },
    {
        path: "/about",
        name: "About",
        component: () => import("@/views/About.vue"),
    },
    {
        path: "/login",
        name: "Login",
        component: () => import("@/views/Login.vue"),
    },
    {
        path: "/signup",
        name: "Signup",
        component: () => import("@/views/Signup.vue"),
    },
    {
        path: "/search",
        name: "Search",
        component: () => import("@/views/Search.vue"),
    },
    {
        path: "/image/:id",
        name: "ImageDetail",
        component: () => import("@/views/ImageDetail.vue"),
    },
    {
        path: "/user/:id",
        name: "UserDetail",
        component: () => import("@/views/UserDetail.vue"),
    },
    {
        path: "/upload",
        name: "Upload",
        component: () => import("@/views/Upload.vue"),
    },
    {
        path: "/:pathMatch(.*)*",
        name: "NotFound",
        component: () => import("@/views/Home.vue"),
    },
];

const router = createRouter({
    mode: "history",
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
});

// Check can user access to that route
router.beforeEach((to, from, next) => {
    // User store
    const userStore = useUserStore();

    // Variable
    const privatePages = ["UserDetail", "Upload"];
    const authRequired = privatePages.includes(to.name);
    const notLoggedIn = userStore.isEmpty;

    const localSave = userStore.localSave;

    if (authRequired && localSave === null) {
        return next("/login");
    } else {
        // Set user data
        if (notLoggedIn && localSave) userStore.login(localSave);

        // Check route
        if (to.name === "Login" && !userStore.isEmpty) next({ name: "Home" });
        else if (to.name === "Signup" && !userStore.isEmpty)
            next({ name: "Home" });
        else next();
    }
});

export default router;
