import { createRouter, createWebHistory } from "vue-router";
import { useUserStore } from "@/store/useUser";

const routes = [
    {
        path: '/',
        name: 'Home',
        component: () => import('@/views/TheHome.vue')
    },
    {
        path: '/about',
        name: 'About',
        component: () => import('@/views/TheAbout.vue')
    },
    {
        path: '/login',
        name: 'Login',
        component: () => import('@/views/TheLogin.vue')
    },
    {
        path: '/signup',
        name: 'Signup',
        component: () => import('@/views/TheSignup.vue')
    },
    {
        path: '/user/:id',
        name: 'UserDetail',
        component: () => import('@/views/TheUserDetail.vue')
    },
    {
        path: '/:pathMatch(.*)*',
        name: 'NotFound',
        component: () => import('@/views/TheError.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

// Check can user access to that route
router.beforeEach((to, from, next) => {
    // User store
    const userStore = useUserStore();

    // Variable
    const privatePages = ['UserDetail'];
    const authRequired = privatePages.includes(to.name);
    const loggedIn = userStore.localSave;

    if (authRequired && !loggedIn) {
        return next('/login')
    }
    else {
        // Set user data
        if (loggedIn) userStore.setUserDetail(loggedIn)

        // Check route
        if (to.name === 'Login' && !userStore.isEmpty) next({ name: 'Home' })
        else if (to.name === 'Signup' && !userStore.isEmpty) next({ name: 'Home' })
        else next()
    }
});

export default router