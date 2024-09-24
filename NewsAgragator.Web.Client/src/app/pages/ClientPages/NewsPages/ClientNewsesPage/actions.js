export const loadData = () => ({
    type: 'CLIENTNEWS_LOADALL',
    remote: {
        url: '/api/news/takesuitablenewses',
        type: 'get',
    }
});