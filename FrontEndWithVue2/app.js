/* const routes=[
    {path:'/home',component:home},
    {path:'/employee',component:employee},
    {path:'/department',component:department}

] */



// 2. Define some routes
// Each route should map to a component.
// We'll talk about nested routes later.
const routes = [
  { path: '/home', component: home },
  { path: '/department', component: department },
  {path:'/employee',component:employee},
]
const router = VueRouter.createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: VueRouter.createWebHashHistory(),
    routes, // short for `routes: routes`
  })
  const app = Vue.createApp({})
// Make sure to _use_ the router instance to make the
// whole app router-aware.
   app.use(router)

app.mount('#app')