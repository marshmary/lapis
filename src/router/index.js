import { createRouter, createWebHistory } from "vue-router";

const routes = [
    {
        path: '/',
        name: 'Home',
        component: () => import('../views/TheHome.vue')
    },
    {
        path: '/about',
        name: 'About',
        component: () => import('../views/TheAbout.vue')
    },
    {
        path: '/login',
        name: 'Login',
        component: () => import('../views/TheLogin.vue')
    },
    {
        path: '/signup',
        name: 'Signup',
        component: () => import('../views/TheSignup.vue')
    },
    {
        path: '/users/:id',
        name: 'UserDetail',
        component: () => import('../views/TheUserDetail.vue')
    },
    {
        path: '/:pathMatch(.*)*',
        name: 'NotFound',
        component: () => import('../views/TheError.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router