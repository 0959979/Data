def testingequalness(k):
    def f_(f,g):
        return all(f(x) == g(x) for x in range(-k,k+1))
        return f_
def f(x):
    return x if x &gt;= 0 else -x
equal100 = testingequalness(100)
print(&#39;f is equal to abs: &#39;, equal100(f,abs))