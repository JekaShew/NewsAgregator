import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "ROLE_LOADALL_SUCCESS":
            return {
                ...state,
                roles:
                {
                    loading: false,
                    value: action.data,
                }
            }

        case "ROLE_LOADALL_START":
            return {
                ...state,
                roles:
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
                                policies:[],
                            },
                        ],
                }
            }

        case "ROLE_REMOVE_START":
            return {
                ...state,
                roles:
                {
                    ...state.roles,
                    loading: true,
                }
            }

        default:
            return state
    }
}