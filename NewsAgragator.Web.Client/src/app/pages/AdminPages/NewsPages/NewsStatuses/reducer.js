import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NEWSSTATUS_LOADALL_SUCCESS":
            return {
                ...state,
                newsStatuses:
                {
                    loading: false,
                    value: action.data,
                }
            }

        case "NEWSSTATUS_LOADALL_START":
            return {
                ...state,
                newsStatuses:
                {
                    loading: true,
                    value:
                        [
                            {
                                id: {
                                    value: '',
                                },
                                title: {
                                    value: '',
                                },
                                description: {
                                    value: '',
                                },
                            },
                        ],
                }
            }

        case "NEWSSTATUS_REMOVE_START":
            return {
                ...state,
                newsStatuses:
                {
                    ...state.newsStatuses,
                    loading: true,
                }
            }

        default:
            return state
    }
}