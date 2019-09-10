def p(k):
    def p_(f):
        return {f(x) for x in range(-k,k)}
    return p_
g = p(3)
print(g(lambda x: x*x))