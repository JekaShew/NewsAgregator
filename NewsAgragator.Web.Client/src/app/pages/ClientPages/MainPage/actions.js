export const loadData = () => ({
    type: 'MAIN_LOADALL',
    remote: {
        url: '/api/news/taketopnewses',
        type: 'get',
    }
});

