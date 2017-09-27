# 今日头条全站新闻文章数据爬取
url:https://www.toutiao.com/api/pc/feed/

拼接参数：
- `category`：见下[category](#category)表格
- `utm_source`：
- `widen`：
- `max_behot_time`：
- `max_behot_time_tmp`：
- `tadrequire`：
- `as`：
- `cp`：

<h2 id="category">category 列表</h2>

| 标签 | category值 |
| ------------- |:-------------:|
| 推荐 | \_\_all\_\_ |
| 热点 | news_hot |
| 科技 | news_tech |
| 社会 | news_society |
| 娱乐 | news_entertainment |
| 游戏 | news_game|
| 体育 | news_sports |
| 汽车 | news_car |
| 财经 | news_finance |
| 搞笑 | funny |
| 段子 | essay_joke|
| 军事 | news_military |
| 国际 | news_world |
| 时尚 | news_fashion |
| 旅游 | news_travel |
| 探索 | news_discovery |
| 育儿 | news_baby |
| 养生 | news_regimen |
| 美文 | news_essay |
| 历史 | news_history |
| 美食 | news_food |
| ... | ... |

json接口示例:https://www.toutiao.com/api/pc/feed/?category=news_hot